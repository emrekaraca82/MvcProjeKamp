using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    interface ISliderService
    {
        List<Slider> GetList();
        void SliderAdd(Slider slider);
        Slider GetById(int id);
        void SliderDelete(Slider slider);
        void SliderUpdate(Slider slider);
    }
}
