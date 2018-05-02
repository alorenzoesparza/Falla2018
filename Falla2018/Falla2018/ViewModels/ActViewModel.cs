namespace Falla2018.ViewModels
{
    using Models;

    public class ActViewModel
    {
        #region Propiedades
        public Act Act { get; set; }
        #endregion

        #region Constructor
        public ActViewModel(Act act)
        {
            this.Act = act;
        }
        #endregion
    }
}
