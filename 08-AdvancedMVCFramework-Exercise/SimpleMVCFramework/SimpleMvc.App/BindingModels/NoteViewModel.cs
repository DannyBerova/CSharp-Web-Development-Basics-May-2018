namespace SimpleMvc.App.BindingModels
{
    using System;

    public class NoteViewModel
    {
        public int NoteId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DateOfCreation { get; set; }

        public bool IsFinished { get; set; }
    }
}
