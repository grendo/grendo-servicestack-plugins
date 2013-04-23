using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Bm.Servicestack.Plugins.Tests
{
    [TestFixture]
    public class JsonPrettyPrinterBehavior
    {
        private static readonly string NewLine = Environment.NewLine;
        private const string ComplexJsonLintExamplePath = "..\\..\\TestFiles\\jsonLintBeautifyExample.json";

        private readonly string _basicPrettyPrintArrayInObjectExample =
            "{" + NewLine +
            "    \"CreatedDate\": \"\\/Date(1262325600000)\\/\"," + NewLine +
            "    \"Id\": \"7df51e04-ca58-4804-82f6-e0af2f1d5265\"," + NewLine +
            "    \"Names\": [" + NewLine +
            "        \"One\"," + NewLine +
            "        \"Two\"," + NewLine +
            "        \"Three\"" + NewLine +
            "    ]" + NewLine +
            "}";

        private readonly string _jsonLintVersionOfSimpleObject = "{" + NewLine + "    \"Hammer\": \"throw\"" + NewLine +
                                                                 "}";


        private static TestLeafObject CreateTestLeafObject()
        {
            return new TestLeafObject
                {
                    Id = new Guid("7df51e04-ca58-4804-82f6-e0af2f1d5265"),
                    Names = new[] {"One", "Two", "Three"},
                    CreatedDate = new DateTime(2010, 1, 1)
                };
        }

        private static TestRootObject GenerateComplexTestObject()
        {
            return new TestRootObject
                {
                    Leaves =
                        new List<TestLeafObject>
                            {
                                CreateTestLeafObject(),
                                CreateTestLeafObject(),
                                CreateTestLeafObject()
                            },
                    Id = new Guid("325116C6-5147-4f09-8B9D-5547359A5153"),
                    CreatedDate = new DateTime(2010, 4, 14),
                    Friend = CreateTestLeafObject(),
                    Titles = new[] {"Crazy horse's weeding", "\"c:\\windows\\home\"", "happy"}
                };
        }


        private static void SerializeAndCompareTheTwoStrings<T>(T testObj, string example)
        {
            var unprettyString = ServiceStack.Text.JsonSerializer.SerializeToString(testObj, testObj.GetType());
            //var unprettyString = testObj.ToJSON();

            ComparePrettyPrintResultWithExample(testObj, unprettyString, example);
        }

        private static void ComparePrettyPrintResultWithExample<T>(T testObj, string unprettyString, string example)
        {
            string prettyString = unprettyString.PrettyPrintJson();
            var deserializedObject = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(prettyString);
            //var deserializedObject = prettyString.DeserializeFromJson<T>();
            Assert.That(deserializedObject, Is.EqualTo(testObj));

            ComparePrettyResultWithExampleResult(unprettyString, prettyString, example);
        }

        private static void ComparePrettyResultWithExampleResult(string original, string prettyString, string example)
        {
            Console.WriteLine("Original String:\n" + original);
            Assert.That(prettyString, Is.Not.EqualTo(string.Empty));
            Assert.That(prettyString, Is.EqualTo(example));
            Console.WriteLine("Beautified string:");
            Console.Write(prettyString);
        }

        [Test]
        public void should_be_able_to_handle_an_empty_object()
        {
            string example = "{" + NewLine + "}";

            const string originalString = "{}";

            string prettyString = originalString.PrettyPrintJson();

            ComparePrettyResultWithExampleResult(originalString, prettyString, example);
        }


        [Test]
        public void should_be_able_to_pretty_print_a_simple_object()
        {
            var testObj = new SimpleObject {Hammer = "throw"};

            var json = ServiceStack.Text.JsonSerializer.SerializeToString(testObj, testObj.GetType());
            var unprettyString = json.Replace(":", " :" + NewLine + NewLine + NewLine);

            ComparePrettyPrintResultWithExample(testObj, unprettyString, _jsonLintVersionOfSimpleObject);
        }

        [Test]
        public void should_beautify_nested_objects_correctly()
        {
            TestRootObject testComplexObject = GenerateComplexTestObject();

            string complexTestString = File.ReadAllText(ComplexJsonLintExamplePath);

            SerializeAndCompareTheTwoStrings(testComplexObject, complexTestString);
        }

        [Test]
        public void should_pretty_print_json_an_object_with_complex_members()
        {
            TestLeafObject testObj = CreateTestLeafObject();

            SerializeAndCompareTheTwoStrings(testObj, _basicPrettyPrintArrayInObjectExample);
        }
    }
}