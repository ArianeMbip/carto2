//using DossierPortailAPI.Services.Gotenberg.Ressources;
using ApiCartobani.Gotenberg.Resources;

namespace ApiCartobani.Gotenberg.Commands
{
    public abstract class BasePdfGotenbergCommand : BaseCommand
    {
        public GotenbergChromiumConvertProperties PdfProperties { get; set; }
    }
}