using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage(" Adı Boş Geçilemez");
            RuleFor(x => x.WriterSurName).NotEmpty().WithMessage("Soyadı Adı Boş Geçilemez");
            RuleFor(x => x.WriterEmail).NotEmpty().WithMessage("Email Adı Boş Geçilemez");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkında Boş Geçilemez");
            RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Başlık Boş Geçilemez");
            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifre Boş Geçilemez");
        }
    }
}
