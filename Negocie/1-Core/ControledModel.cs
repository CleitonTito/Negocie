using FluentValidation;
using FluentValidation.Results;

namespace Negocie._1_Core
{
    public abstract class ControledModel
    {
        public virtual ValidationResult ValidationResult { get; protected set; }

        public virtual bool Valid { get; protected set; }

        public virtual bool Validate<T>(T model, IValidator validator, params string[] ruleSets)
        {
            var context = ValidationContext<T>.CreateWithOptions(model, options =>
            {
                options.IncludeRulesNotInRuleSet();

                if (ruleSets?.Any() ?? false)
                    options.IncludeRuleSets(ruleSets);
            });

            ValidationResult = validator.Validate(context);
            return Valid = ValidationResult.IsValid;
        }

        public abstract class ControleModel<TValidator> : ControledModel where TValidator : IValidator, new()
        {
            protected bool Validate(params string[] ruleSets)
            {
                IValidator validator = new TValidator();
                return Validate(this, validator, ruleSets);
            }
        }
    }
}
