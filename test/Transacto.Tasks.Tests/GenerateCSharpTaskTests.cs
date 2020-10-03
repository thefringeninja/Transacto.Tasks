using System.Collections;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Xunit;

namespace Transacto.Tasks {
	public class GenerateCSharpTaskTests {
		private readonly GenerateCSharpTask _sut;

		public GenerateCSharpTaskTests() {
			_sut = new GenerateCSharpTask {
				RootNamespace = nameof(GenerateCSharpTask.RootNamespace),
				MSBuildProjectDirectory = string.Empty,
				SchemaFiles = new DirectoryInfo(Directory.GetCurrentDirectory())
					.GetFiles("*.schema.json", SearchOption.TopDirectoryOnly)
					.Select(f => (ITaskItem)new TestTaskItem(f.FullName))
					.ToArray(),
				IntermediateOutputPath = Directory.GetCurrentDirectory()
			};
		}

		[Fact]
		public void DoesNotCrash() {
			Assert.True(_sut.Execute());
		}

		private class TestTaskItem : ITaskItem {
			private readonly string _path;

			public TestTaskItem(string path) {
				_path = path;
			}

			public string GetMetadata(string metadataName) => throw new System.NotImplementedException();

			public void SetMetadata(string metadataName, string metadataValue) =>
				throw new System.NotImplementedException();

			public void RemoveMetadata(string metadataName) => throw new System.NotImplementedException();
			public void CopyMetadataTo(ITaskItem destinationItem) => throw new System.NotImplementedException();
			public IDictionary CloneCustomMetadata() => throw new System.NotImplementedException();
			public string ItemSpec { get; set; }
			public ICollection MetadataNames { get; }
			public int MetadataCount { get; }
			public override string ToString() => _path;
		}
	}
}
