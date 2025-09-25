using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardTesterLibrary
{
    public static class CodingResultExtensions
    {
        /// <summary>
        /// Converts a CodingResult enum value to a user-friendly string representation.
        /// </summary>
        /// <param name="result">The CodingResult enum value to convert.</param>
        /// <returns>A string representation of the CodingResult.</returns>
        public static string ToDisplayString(this CodingResult result)
        {
            switch (result)
            {
                case CodingResult.CODING_RESULT_SYSTEM_ERROR:
                    return "System Error - Process not operational";
                case CodingResult.CODING_RESULT_REJECT:
                    return "Reject - Coding result was bad";
                case CodingResult.CODING_RESULT_UNINITIALIZED:
                    return "Uninitialized - Default value";
                case CodingResult.CODING_RESULT_OK:
                    return "OK - Coding successful";
                default:
                    return $"Unknown CodingResult: {result}";
            }
        }

        /// <summary>
        /// Converts a CodingResult enum value to a short string representation.
        /// </summary>
        /// <param name="result">The CodingResult enum value to convert.</param>
        /// <returns>A short string representation of the CodingResult.</returns>
        public static string ToShortString(this CodingResult result)
        {
            switch (result)
            {
                case CodingResult.CODING_RESULT_SYSTEM_ERROR:
                    return "System Error";
                case CodingResult.CODING_RESULT_REJECT:
                    return "Reject";
                case CodingResult.CODING_RESULT_UNINITIALIZED:
                    return "Uninitialized";
                case CodingResult.CODING_RESULT_OK:
                    return "OK";
                default:
                    return result.ToString();
            }
        }

        /// <summary>
        /// Converts a string to a CodingResult enum value.
        /// Supports parsing from display strings, short strings, enum names, and numeric values.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The corresponding CodingResult enum value.</returns>
        /// <exception cref="ArgumentException">Thrown when the string cannot be converted to a valid CodingResult.</exception>
        public static CodingResult ToCodingResult(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            string trimmedValue = value.Trim();

            // Try parsing as enum name first
            if (Enum.TryParse<CodingResult>(trimmedValue, true, out CodingResult enumResult))
                return enumResult;

            // Try parsing as numeric value
            if (int.TryParse(trimmedValue, out int numericValue))
            {
                if (Enum.IsDefined(typeof(CodingResult), numericValue))
                    return (CodingResult)numericValue;
            }

            // Try parsing from display strings (case-insensitive)
            switch (trimmedValue.ToLowerInvariant())
            {
                case "system error":
                case "system error - process not operational":
                    return CodingResult.CODING_RESULT_SYSTEM_ERROR;
                
                case "reject":
                case "reject - coding result was bad":
                    return CodingResult.CODING_RESULT_REJECT;
                
                case "uninitialized":
                case "uninitialized - default value":
                    return CodingResult.CODING_RESULT_UNINITIALIZED;
                
                case "ok":
                case "ok - coding successful":
                    return CodingResult.CODING_RESULT_OK;
                
                default:
                    throw new ArgumentException($"Unable to convert '{value}' to CodingResult.", nameof(value));
            }
        }

        /// <summary>
        /// Tries to convert a string to a CodingResult enum value.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="result">When this method returns, contains the CodingResult value if conversion succeeded, or the default value if it failed.</param>
        /// <returns>true if the conversion succeeded; otherwise, false.</returns>
        public static bool TryParseCodingResult(this string value, out CodingResult result)
        {
            result = default(CodingResult);

            try
            {
                result = value.ToCodingResult();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}