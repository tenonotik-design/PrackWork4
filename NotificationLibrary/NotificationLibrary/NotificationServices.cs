namespace NotificationLibrary
{
    public class NotificationServices
    {
        public IEnumerable<Notification> FilterAndSort(
            IEnumerable<Notification> notifications,
            NotificationFilterOptions options)
        { 
            if (notifications == null) throw new ArgumentNullException(nameof(notifications));
            if (options == null) throw new ArgumentNullException(nameof(options));

            var query = notifications.AsQueryable();

            if (options.IsRead.HasValue)
                query = query.Where(n => n.IsRead == options.IsRead.Value);

            if (options.Types != null && options.Types.Length > 0)
                query = query.Where(n => options.Types.Contains(n.Type));

            if (options.MinPriority.HasValue)
                query = query.Where(n => n.Priority >= options.MinPriority.Value);

            if (!string.IsNullOrWhiteSpace(options.SearchText))
            {
                query = query.Where(n =>
                    n.Title.Contains(options.SearchText, StringComparison.OrdinalIgnoreCase) ||
                    (n.Content != null && n.Content.Contains(options.SearchText, StringComparison.OrdinalIgnoreCase)));
            }

            if (options.SortBy.HasValue)
            {
                bool desc = options.Descending;

                query = options.SortBy.Value switch
                {
                    SortNotificationBy.Date => desc
                        ? query.OrderByDescending(n => n.CreatedAt)
                        : query.OrderBy(n => n.CreatedAt),

                    SortNotificationBy.Priority => desc
                        ? query.OrderByDescending(n => n.Priority)
                        : query.OrderBy(n => n.Priority),

                    SortNotificationBy.Title => desc
                        ? query.OrderByDescending(n => n.Title)
                        : query.OrderBy(n => n.Title),

                    _ => query
                };
            }

            return query.ToList();
        }
    }
}
