using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace ValidateThis;

//Creacion del Cacaolat, dentro tiene Data Annotations (no por mucho tiempo)
public class Cacaolat
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(50,ErrorMessage = "El nombre no puede superar los 50 chars")]
    public string Name { get; set; }
    
    
    [Range(0.3,2.5, ErrorMessage = "El tamaño debe estar entre 0.3L y 2.5L")]
    public double Size { get; set; }
    
    public bool estaBueno { get; set; }
}
//Metodo CUSTOM VALIDATOR de Size, que no sea menos de 0.3L
public class CacaolatSize
{
    public static ValidationResult ValidateSize(Cacaolat cacaolat, ValidationContext context)
    {
        if (cacaolat.Size < 0.3)
        {
            return new ValidationResult("Cacaolat molt petit noi");
        }
        return ValidationResult.Success;
    }
}
//Metodo CUSTOM VALIDATOR de Name, que no esa null or empty y que tenga entre 3 y 20 chars
public class CacaolatName
{
    public static ValidationResult ValidateName(Cacaolat cacaolat, ValidationContext context)
    {
        if (string.IsNullOrEmpty(cacaolat.Name))
        {
            return new ValidationResult("Cacaolat DEBE EXISTIR");
        }

        if (cacaolat.Name.Length <3 || cacaolat.Name.Length > 20)
        {
            return new ValidationResult("El nombre del cacaolat debe estar entre 3 y 20 caracteres");
        }
        return ValidationResult.Success;
    }
}

public class CacaolatValidator : AbstractValidator<Cacaolat>
{
    public CacaolatValidator()
    {
        RuleFor(Cacaolat => Cacaolat.Name).NotEmpty().WithMessage("El nombre es obligatorio").Length(3, 20)
            .WithMessage("El nombre debe contener entre 3 y 20 chars");
        RuleFor(Cacaolat => Cacaolat.Size).GreaterThan(0.29).WithMessage("Cacaolat DEBE ser da mas de 0.3L");
        
        RuleFor(Cacaolat => Cacaolat.estaBueno).Must(estaBueno => estaBueno).WithMessage("COMO QUE NO ESTA BUENO !!!!!1!!");
        
        
        
        
        
        

    }
}




public class Program
{
    public static void Main()
    {
        
        //Este bloque es de Data Annotations
        Cacaolat cacaolat = new Cacaolat { Name = "Cacaolat OG", Size = 0.30, estaBueno = true};
        
        /*ValidationContext context = new ValidationContext(cacaolat);
        var results = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(cacaolat, context, results, true);
        if (!isValid)
        {
            foreach (var validationResult in results)
            {
                Console.WriteLine(validationResult.ErrorMessage);
            }
        }
        else
        {
            Console.WriteLine("Todo flama ");
        }*/
        
        //Este bloque es de CUSTOM VALIDATIONS
        /*
        var cacaolatSizeResult = CacaolatSize.ValidateSize(cacaolatCustom, context);
        if (cacaolatSizeResult != ValidationResult.Success)
        {
            results.Add(cacaolatSizeResult);
        }

        var cacaolatNameResult = CacaolatName.ValidateName(cacaolatCustom, context);
        if (cacaolatNameResult != ValidationResult.Success)
        {
            results.Add(cacaolatNameResult);
        }
        if (results.Count>0)
        {
            foreach (var validationResult  in results)
            {
                Console.WriteLine(validationResult.ErrorMessage);
            }
        }
        else
        {
            Console.WriteLine("Disfruta del teu cacaolat");
        }
        */

        //Este Bloque es de FluentValidations
        
        CacaolatValidator cacaolatValidator = new CacaolatValidator();
        var results = cacaolatValidator.Validate(cacaolat);
        if (!results.IsValid)
        {
            foreach (var validationResult in results.Errors)
            {
                Console.WriteLine(validationResult.ErrorMessage);
            }
        }
        else
        {
            Console.WriteLine("Todo flama ");
        }
        

    }
}