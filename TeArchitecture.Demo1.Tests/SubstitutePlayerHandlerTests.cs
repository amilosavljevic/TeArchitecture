using System.Linq;
using NUnit.Framework;
using TeArchitecture.Domain;
using TeArchitecture.Shared;
using TeArchitecture.Shared.Mock;

namespace TeArchitecture.Demo1.Tests
{
    public class SubstitutePlayerHandlerTests
    {
        private const int AllPlayersCount = 22;
        private const int PlayersOnPitchCount = 11;

        [SetUp]
        public void Setup() {}

        [Test]
        public void TestHappyPath()
        {
            // Create defualt squad and then swap player at index 0
            // with player at index 1. Both are on pitch.

            var squad = GenerateSquad();
            var bus = new BusMock();

            // Fake channel with fake instant response
            var channel = new ChannelMock();
            channel.Mock((SubstitutePlayerRequest req) => new SubstitutePlayerResponse() {IsSuccess = true});

            var action = new SubstitutePlayersAction (new PlayerId(0), new PlayerId(1));

            // Check before
            CheckSquad(squad);
            Assert.AreEqual(0, (long) squad.PlayersOnPitch[0]);
            Assert.AreEqual(1, (long) squad.PlayersOnPitch[1]);

            var handler = new SubstitutePlayerHandler(bus, squad, channel);
            var task = handler.Process(action);

            // Check after
            CheckSquad(squad);

            Assert.AreEqual(TaskState.Successful, task.State);

            Assert.AreEqual(0, (long)squad.PlayersOnPitch[1]);
            Assert.AreEqual(1, (long)squad.PlayersOnPitch[0]);

            // Check that we notify rest of system that squad is changed.
            Assert.AreEqual(1, bus.SentMessages.Count);
            Assert.AreEqual(typeof(SquadUpdatedEvent), bus.SentMessages[0].GetType());
        }

        [Test]
        public void TestHappyPath2()
        {
            // Create defualt squad and then swap player at index 0
            // with player at index 10. Both are on pitch.

            var squad = GenerateSquad();
            var bus = new BusMock();

            // Fake channel with fake instant response
            var channel = new ChannelMock();
            channel.Mock((SubstitutePlayerRequest req) => new SubstitutePlayerResponse() { IsSuccess = true });

            var action = new SubstitutePlayersAction(new PlayerId(0), new PlayerId(10));

            // Check before
            CheckSquad(squad);
            Assert.AreEqual(0, (long)squad.PlayersOnPitch[0]);
            Assert.AreEqual(10, (long)squad.PlayersOnPitch[10]);

            var handler = new SubstitutePlayerHandler(bus, squad, channel);
            var task = handler.Process(action);

            // Check after
            CheckSquad(squad);

            Assert.AreEqual(TaskState.Successful, task.State);
            Assert.AreEqual(0, (long)squad.PlayersOnPitch[10]);
            Assert.AreEqual(10, (long)squad.PlayersOnPitch[0]);

            // Check that we notify rest of system that squad is changed.
            Assert.AreEqual(1, bus.SentMessages.Count);
            Assert.AreEqual(typeof(SquadUpdatedEvent), bus.SentMessages[0].GetType());
        }

        [Test]
        public void TryToSwapTwoPlayersThatAreNotOnThePitch()
        {
            var squad = GenerateSquad();
            var bus = new BusMock();

            // Fake channel with fake instant response
            var channel = new ChannelMock();
            channel.Mock((SubstitutePlayerRequest req) => new SubstitutePlayerResponse() { IsSuccess = true });

            var action = new SubstitutePlayersAction(new PlayerId(20), new PlayerId(21));

            // Check before
            CheckSquad(squad);
            Assert.IsFalse(squad.PlayersOnPitch.Contains(action.Player1));
            Assert.IsFalse(squad.PlayersOnPitch.Contains(action.Player2));

            var handler = new SubstitutePlayerHandler(bus, squad, channel);
            var task = handler.Process(action);

            // Check after
            CheckSquad(squad);
            CheckThatSquadIsNotChanged(squad);

            Assert.AreEqual(TaskState.Failed, task.State);
            Assert.AreEqual(SubstitutePlayerHandler.PlayersNotOnPitch, task.Error);
        }

        [Test]
        public void TryToSwapTwoPlayersWithSameOne()
        {
            var squad = GenerateSquad();
            var bus = new BusMock();

            // Fake channel with fake instant response
            var channel = new ChannelMock();
            channel.Mock((SubstitutePlayerRequest req) => new SubstitutePlayerResponse() { IsSuccess = true });

            var action = new SubstitutePlayersAction(new PlayerId(0), new PlayerId(0));

            // Check before
            CheckSquad(squad);
            Assert.IsTrue(squad.PlayersOnPitch.Contains(action.Player1));
            Assert.IsTrue(squad.PlayersOnPitch.Contains(action.Player2));
            Assert.AreEqual(action.Player1, action.Player2);

            var handler = new SubstitutePlayerHandler(bus, squad, channel);
            var task = handler.Process(action);

            // Check after
            CheckSquad(squad);
            CheckThatSquadIsNotChanged(squad);

            Assert.AreEqual(TaskState.Failed, task.State);
            Assert.AreEqual(SubstitutePlayerHandler.CannotSwapWithSamePlayer, task.Error);
        }

        [Test]
        public void TryToSwapTwoPlayersThatWeDoNotHave()
        {
            var squad = GenerateSquad();
            var bus = new BusMock();

            // Fake channel with fake instant response
            var channel = new ChannelMock();
            channel.Mock((SubstitutePlayerRequest req) => new SubstitutePlayerResponse() { IsSuccess = true });

            var action = new SubstitutePlayersAction(new PlayerId(0), new PlayerId(100));

            // Check before
            CheckSquad(squad);
            Assert.IsTrue(squad.PlayersOnPitch.Contains(action.Player1));
            Assert.IsFalse(squad.PlayersOnPitch.Contains(action.Player2));

            var handler = new SubstitutePlayerHandler(bus, squad, channel);
            var task = handler.Process(action);

            // Check after
            CheckSquad(squad);
            CheckThatSquadIsNotChanged(squad);

            Assert.AreEqual(TaskState.Failed, task.State);
            Assert.AreEqual(SubstitutePlayerHandler.PlayerNotPartOfSquad, task.Error);
        }

        [Test]
        public void ServerFailsToProcessRequest()
        {
            var squad = GenerateSquad();
            var bus = new BusMock();

            // Fake channel with fake instant response
            var channel = new ChannelMock();
            channel.Mock((SubstitutePlayerRequest req) => new SubstitutePlayerResponse() { IsSuccess = false });

            var action = new SubstitutePlayersAction(new PlayerId(0), new PlayerId(1));

            // Check before
            CheckSquad(squad);            

            var handler = new SubstitutePlayerHandler(bus, squad, channel);
            var task = handler.Process(action);

            // Check after
            CheckSquad(squad);
            CheckThatSquadIsNotChanged(squad);

            Assert.AreEqual(TaskState.Failed, task.State);
            Assert.AreEqual(SubstitutePlayerHandler.FailToSubstituePlayersOnServer, task.Error);
        }

        private Squad GenerateSquad()
        {
            return new Squad()
            {
                AllPlayers = Enumerable.Range(0, AllPlayersCount)
                            .Select(id => new Player()
                            {
                                Id = new PlayerId(id),
                                Name = "Player " + id,
                                Age = 18 + id % 18,
                                Condition = new Condition(100),
                                Moral = new Moral(10),                                
                            }).ToList(),
                PlayersOnPitch = Enumerable.Range(0, PlayersOnPitchCount)
                                .Select(id=> new PlayerId(id))
                                .ToList(),
            };
        }

        private void CheckSquad(Squad squad)
        {
            Assert.IsNotNull(squad.AllPlayers);
            Assert.AreEqual(AllPlayersCount, squad.AllPlayers.Count);

            Assert.IsNotNull(squad.PlayersOnPitch);
            Assert.AreEqual(PlayersOnPitchCount, squad.PlayersOnPitch.Count);
        }

        private void CheckThatSquadIsNotChanged(Squad squad)
        {
            for (int i = 0; i < squad.AllPlayers.Count; i++)
            {
                Assert.AreEqual(i, (long)squad.AllPlayers[i].Id);
            }

            for (int i = 0; i < squad.PlayersOnPitch.Count; i++)
            {
                Assert.AreEqual(i, (long)squad.PlayersOnPitch[i]);
            }
        }
    }
}