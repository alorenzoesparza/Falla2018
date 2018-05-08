namespace Falla2018.Domain
{
    public class Response
    {
        /*
         * Esta clase la utilizamos para las respuestas 
         * de un servicio.
        */

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }
    }
}
