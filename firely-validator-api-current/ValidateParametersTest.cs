using Firely.Fhir.Validation;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using System.ComponentModel.DataAnnotations;

namespace firely_validator_api_current
{
    public class ValidateParametersTest
    {
        [Test]
        public void Parameters_Should_Fail_Due_To_Empty_Value()
        {
            var profileResolver = ZipSource.CreateValidationSource();
            var terminologySource = new LocalTerminologyService(profileResolver);
            var validator = new Firely.Fhir.Validation.Validator(profileResolver,
                                          terminologySource,
                                          null,
                                          new ValidationSettings());

            var resource = new Parameters();
            resource.Parameter.Add(
                new Parameters.ParameterComponent()
                {
                    Name = "temp-name",
                    Part = new List<Parameters.ParameterComponent>(new[]
                    {
                        new Parameters.ParameterComponent()
                        {
                            Name = "invalid"
                        },
                    })
                }
            );

            var oo = validator.Validate(resource);
            Assert.IsTrue(oo.Success == false);
            Assert.NotZero(oo.Issue.Count);
        }
    }
}