using System.Linq;

namespace Catalog.API.Products.GetAllCategories
{
    public record GetUniqueCategoriesQuery() : IQuery<List<string>>;

    public class GetUniqueCategoriesHandler : IQueryHandler<GetUniqueCategoriesQuery, List<string>>
    {
        private readonly IDocumentSession _session;

        public GetUniqueCategoriesHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<List<string>> Handle(GetUniqueCategoriesQuery request, CancellationToken cancellationToken)
        {
            var sql = @"
            select distinct jsonb_array_elements_text(data->'Category') as category
            from mt_doc_product
            order by category;
        ";

            var categories = _session
                .Query<string>(sql)
                .ToList(); // <- OVO JE KLJUČNO

            return categories;
        }
    }

}
