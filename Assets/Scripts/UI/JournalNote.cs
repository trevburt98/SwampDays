public class JournalNote
{
    public int Id;
    public string Title;
    public string Content;

    public bool saved;

    public JournalNote(int id){
        Id = id;
        Title = "New Note " + id.ToString();
        Content = "";
        saved = false;
    }
}
