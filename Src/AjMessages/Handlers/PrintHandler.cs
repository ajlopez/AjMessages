using System;
using System.Collections.Generic;
using System.Text;

namespace AjMessages
{
    class PrintHandler : IHandler
    {
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
	

        #region IHandler Members

        public void Process(Context ctx)
        {
            Console.WriteLine(text);
        }

        #endregion
    }
}
