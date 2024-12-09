using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Validation;

namespace firely_validator_api_legacy
{
    public class ValidateParametersTest
    {
        [Test]
        public void Test1()
        {
            var profileResolver = ZipSource.CreateValidationSource();
            var terminologySource = new LocalTerminologyService(profileResolver);
            ValidationSettings settings = new ValidationSettings
            {
                ResourceResolver = profileResolver,
                TerminologyService = terminologySource,
            };
            var validator = new Hl7.Fhir.Validation.Validator(settings);

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
            var str = new FhirJsonSerializer(new SerializerSettings() { Pretty = true }).SerializeToString(oo);
            Assert.IsTrue(oo.Success == false);
            Assert.NotZero(oo.Issue.Count);
        }
    }
}