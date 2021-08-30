using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class SliderManager : ISliderService
    {
        ISliderDal _sliderDal;

        public SliderManager(ISliderDal sliderDal)
        {
            _sliderDal = sliderDal;
        }

        public Slider GetById(int id)
        {
            return _sliderDal.Get(x => x.SliderId == id);
        }

        public List<Slider> GetList()
        {
           return _sliderDal.List();
        }

        public void SliderAdd(Slider slider)
        {
            _sliderDal.Insert(slider);
        }

        public void SliderDelete(Slider slider)
        {
            _sliderDal.Delete(slider);
        }

        public void SliderUpdate(Slider slider)
        {
            _sliderDal.Update(slider);
        }
    }
}
