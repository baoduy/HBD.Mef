#region using

using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace AzureStorage.Tests
{
    [TestClass]
    public class DocumentStorageTests
    {
        [TestMethod]
        public async Task GetAllAsyncTest()
        {
            using (var doc = new NoteStorage())
            {
                var rs = await doc.GetAllAsync();

                Assert.IsTrue(rs.Count > 0);
            }
        }

        [TestMethod]
        public async Task GetByIdAsyncTest()
        {
            using (var doc = new NoteStorage())
            {
                var newnote = await doc.CreateNewAsync();
                newnote.Title = "HBD";
                await doc.SaveAsync(newnote);

                Assert.IsTrue((await doc.GetByIdAsync(newnote.Id)).Title == newnote.Title);
            }
        }

        [TestMethod]
        public async Task CreateNewAsyncTest()
        {
            using (var doc = new NoteStorage())
            {
                var newnote = await doc.CreateNewAsync();
                newnote.Title = "HBD";
                var result = await doc.SaveAsync(newnote);

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public async Task UpdateAsyncTest()
        {
            using (var doc = new NoteStorage())
            {
                var newnote = (await doc.GetAllAsync()).First();
                newnote.Title = "Duy";
                var result = await doc.SaveAsync(newnote);

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public async Task DeleteAsyncTest()
        {
            using (var doc = new NoteStorage())
            {
                var newnote = await doc.CreateNewAsync();
                newnote.Title = "HBD";
                await doc.SaveAsync(newnote);

                Assert.IsTrue(await doc.DeleteAsync(newnote));
            }
        }

        [TestMethod]
        public async Task DeleteAllAsyncTest()
        {
            using (var doc = new NoteStorage())
            {
                await doc.DeleteAllAsync();

                Assert.IsTrue((await doc.GetAllAsync()).Count==0);
            }
        }
    }
}