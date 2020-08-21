using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife.UI
{
    public interface IView
    {
        void Render(Map map);
    }
}
