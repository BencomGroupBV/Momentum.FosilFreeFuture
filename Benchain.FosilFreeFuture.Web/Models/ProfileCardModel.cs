namespace Benchain.FosilFreeFuture.Web.Models
{
  public class ProfileCardModel
  {
    public string CardTitle { get; set; }
    public bool ShowAddProfile { get; set; }
    public bool ShowHasMail { get; set; }
    public ProfileModel? Profile { get; set; }
  }
}
