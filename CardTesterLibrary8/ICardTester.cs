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
        /// <returns></returns>
        public Task RunTestAsync(int numberOfCycles, int delayUp, int delayDown);

        /// <summary>
        /// Moves piston to lower position in case it is stopped.
        /// </summary>
        /// <returns></returns>
        public Task ResetDownAsync();

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
    }
}
