namespace InsuranceCompany.ViewModels {
    public class PageModelWithAggregateValue<T, K, E> : PageModel<T, K> {
        public E AggregateValue { get; set; }

        public PageModelWithAggregateValue(int page, int pageSize, IEnumerable<T> entities, K filterModel, E aggregateValue) : base(page, pageSize, entities, filterModel) {
            AggregateValue = aggregateValue;
        }
    }
}
