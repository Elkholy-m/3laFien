using Entities.Models;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Microsoft.AspNetCore.Hosting;
using Service.Contracts;

namespace Service;

public class SearchService : ISearchService, IDisposable
{

    const LuceneVersion VERSION = LuceneVersion.LUCENE_48;
    const string INDEX_NAME = "PLACE_INDEX";

    private readonly StandardAnalyzer _analyzer;
    private readonly FSDirectory _indexDir;
    private readonly IndexWriter _writer;

    public SearchService(IWebHostEnvironment env)
    {
        _analyzer = new(VERSION);
        _indexDir = FSDirectory.Open(Path.Combine(env.WebRootPath, INDEX_NAME));

        var writerConfig = new IndexWriterConfig(VERSION, _analyzer) { OpenMode = OpenMode.CREATE_OR_APPEND };
        _writer = new(_indexDir, writerConfig);
    }

    public async Task IndexPlace(Place place)
    {
        Document doc = CreateDocument(place);
        _writer.UpdateDocument(new Term("PlaceId", place.PlaceId.ToString()), doc);
        _writer.Flush(triggerMerge: false, applyAllDeletes: false);
        await Task.CompletedTask;
    }

    public async Task DeleteIndex(Guid placeId)
    {
        _writer.DeleteDocuments(new Term("PlaceId", placeId.ToString()));
        _writer.Flush(triggerMerge: false, applyAllDeletes: false);
        await Task.CompletedTask;
    }

    public async Task<IReadOnlyList<Guid>> SearchPlaces(string searchTerm, int maxResults = 1000)
    {
        using IndexReader reader = _writer.GetReader(applyAllDeletes: true);
        IndexSearcher searcher = new(reader);

        // Escape special characters like $ _ # ...etc;
        string escapedTerm = QueryParserBase.Escape(searchTerm.ToLowerInvariant().Trim());

        BooleanQuery booleanQuery = [];
        booleanQuery.Add(new WildcardQuery(new Term("Name", $"*{escapedTerm}*")), Occur.SHOULD);
        booleanQuery.Add(new WildcardQuery(new Term("Description", $"*{escapedTerm}*")), Occur.SHOULD);
        TopDocs hits = searcher.Search(booleanQuery, maxResults);

        var ids = hits.ScoreDocs.Select(hit =>
        {
            var doc = searcher.Doc(hit.Doc);
            return Guid.Parse(doc.Get("PlaceId"));
        }).ToList();

        return await Task.FromResult(ids);
    }

    public async Task RebuildIndex(IEnumerable<Place> places)
    {
        _writer.DeleteAll();

        foreach (var place in places)
        {
            var doc = CreateDocument(place);
            _writer.AddDocument(doc);
        }
        _writer.Commit();
        await Task.CompletedTask;
    }

    private static Document CreateDocument(Place place)
    {
        return [
            new StringField("PlaceId", place.PlaceId.ToString(), Field.Store.YES),

            new TextField("Name", place.Name ?? "", Field.Store.NO),
            new TextField("Description", place.Description ?? "", Field.Store.NO),
        ];
    }

    public void Dispose()
    {
        _analyzer?.Dispose();
        _indexDir?.Dispose();
        _writer?.Dispose();
        GC.SuppressFinalize(this);
    }
}
