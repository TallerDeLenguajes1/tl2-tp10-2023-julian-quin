
namespace tl2_tp10_2023_julian_quin.ViewModels
{
    public class LogedUserViewModel
    {
        public string MensajeDeError;
        public bool TieneMensajeDeError => !string.IsNullOrEmpty(MensajeDeError);
        public bool IsLoged {get;set;}        
        public string Nombre {get;set;}        
        public string NivelAcceso {get;set;}
    }
}