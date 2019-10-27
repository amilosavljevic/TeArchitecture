using System.Linq;
using NUnit.Framework;
using TeArchitecture.Domain;
using TeArchitecture.Shared.Mock;

namespace TeArchitecture.Demo1.Tests
{
    public class SubstitutePlayerHandlerTests
    {
        private const int TotalPlayersCount = 22;
        private const int PlayersOnPitchCount = 11;

        [SetUp]
        public void Setup() {}

        [Test]
        public void TestHappyPath()
        {
            var squad = GenerateSquad();
            var bus = new BusMock();
            var channel = new ChannelMock();
            var action = new SubstitutePlayersAction (new PlayerId(0), new PlayerId(1));


            // Check before
            CheckSquad(squad);
            Assert.AreEqual(0, (long) squad.PlayersOnPitch[0]);
            Assert.AreEqual(1, (long) squad.PlayersOnPitch[0]);

            var handler = new SubstitutePlayerHandler(bus, squad, channel);
            handler.Process(action);

            // Check after
            CheckSquad(squad);
        }

        private Squad GenerateSquad()
        {
            return new Squad()
            {
                AllPlayers = Enumerable.Range(1, TotalPlayersCount)
                            .Select(id => new PlayerData()
                            {
                                Id = new PlayerId(id),
                                Name = "Player " + id,
                                Age = 18 + id % 18,
                                Condition = new Condition(100),
                                Moral = new Moral(10),                                
                            }).ToList(),
                PlayersOnPitch = Enumerable.Range(1, PlayersOnPitchCount)
                                .Select(id=> new PlayerId(id))
                                .ToList(),
            };
        }

        private void CheckSquad(Squad squad)
        {
            Assert.IsNotNull(squad.AllPlayers);
            Assert.AreEqual(TotalPlayersCount, squad.AllPlayers.Count);

            Assert.IsNotNull(squad.PlayersOnPitch);
            Assert.AreEqual(PlayersOnPitchCount, squad.PlayersOnPitch.Count);
        }
    }
}