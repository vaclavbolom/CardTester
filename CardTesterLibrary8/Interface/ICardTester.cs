using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTesterLibrary
{
    /// <summary>
    /// Methods for testing smart cards in bending machine.
    /// </summary>
    public interface ICardTester
    {
        /// <summary>
        /// Runs smart card test
        /// </summary>
        /// <param name="numberOfCycles">Number of bending cycles</param>
        /// <param name="delayUp">Delay in upper position before card test [millisecoinds]</param>
        /// <param name="delayDown">delay in lower position before card test [milliseconds]</param>
        /// <param name="outputPath">directory for protocol</param>
        /// <param name="description">test user description</param>
        /// <param name="cardIds"></param>
        /// <param name="workOrderId"></param>
        /// <returns></returns>
        public Task RunTestAsync(int numberOfCycles, int delayUp, int delayDown, string outputPath, string description, string workOrderId, List<int> cardIds);

        /// <summary>
        /// Moves piston to lower position in case it is stopped. (unbend)
        /// </summary>
        /// <returns></returns>
        public Task ResetDownAsync();

        /// <summary>
        /// Moves piston to upper position in case it is stopped. (bend)
        /// </summary>
        /// <returns></returns>
        public Task ResetUpAsync();

        /// <summary>
        /// Stops piston movement.
        /// </summary>
        /// <returns></returns>
        public Task StopAsync();


        /// <summary>
        /// Prepares measurement:
        /// - reads state of machine
        /// - resets to lower position
        /// </summary>
        /// <returns></returns>
        public Task PrepareMeasurement();


        /// <summary>
        /// Bends the card. Does not check initial position. 
        /// For maintanance operation.
        /// </summary>
        /// <returns></returns>
        public Task MoveForwardUnsafeAsync();


        /// <summary>
        /// Unbends the card. Doues not check initial state.
        /// For maintanance operation.
        /// </summary>
        /// <returns></returns>
        public Task MoveBackwardUnsafeAsync();


        /// <summary>
        /// Counter of test cycles
        /// </summary>
        public int TestCycleIndex { get; }
    }
}
