using System;
using System.Collections.Generic;
using System.Text;

namespace SFML.Graphics
{
    public class ViewRef
    {
        public View View;

        public ViewRef()
        {
        }
        public ViewRef(View view)
        {
            View = view;
        }
    }
}
