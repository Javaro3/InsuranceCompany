namespace Models.ViewModels.PageViewModels {
    public class PageModel<T, K> {
        public IEnumerable<T> Entities { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }

        public K FilterModel { get; set; }

        public PageModel(int page, int pageSize, IEnumerable<T> entities, K filterModel) {
            int totalPages = (int)Math.Ceiling((double)entities.Count() / pageSize);
            page = Math.Max(1, Math.Min(totalPages, page));
            entities = entities
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            PageNumber = page;
            TotalPages = totalPages;
            PageSize = pageSize;
            Entities = entities;
            FilterModel = filterModel;
        }
    }
}
