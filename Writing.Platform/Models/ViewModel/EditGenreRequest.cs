namespace Writing.Platform.Models.ViewModel
{
    public class EditGenreRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
