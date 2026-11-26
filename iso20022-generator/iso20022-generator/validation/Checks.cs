using System;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace iso20022_generator.validation
{
    /// <summary>
    /// Format checks according to the Swiss Payment Standards (SPS 2026, IG Credit Transfer v2.3).
    /// </summary>
    public static class Checks
    {
        // IG chapter 3.2: restricted character set for MsgId, PmtInfId, InstrId and EndToEndId
        private static readonly Regex ReferenceCharacters = new Regex(@"^[A-Za-z0-9 '()+,\-./:?]*$", RegexOptions.Compiled);

        // IG chapter 3.1: Basic Latin, Latin-1 Supplement, Latin Extended-A plus Ș ș Ț ț €
        private static readonly Regex SpsCharacters = new Regex(@"^[ -~ -ſȘ-ț€]*$", RegexOptions.Compiled);

        /// <summary>Removes all spaces and converts to upper case.</summary>
        public static string Compact(string value) => value?.Replace(" ", "").ToUpperInvariant();

        /// <summary>Checks the IBAN structure and its mod-97 check digits (ISO 13616).</summary>
        public static bool IsValidIban(string iban)
        {
            iban = Compact(iban);
            if (string.IsNullOrEmpty(iban) || iban.Length < 15 || iban.Length > 34)
                return false;
            if (!Regex.IsMatch(iban, "^[A-Z]{2}[0-9]{2}[A-Z0-9]+$"))
                return false;

            return Mod97(iban.Substring(4) + iban.Substring(0, 4)) == 1;
        }

        /// <summary>True if the IBAN is a Swiss or Liechtenstein IBAN.</summary>
        public static bool IsChOrLiIban(string iban)
        {
            iban = Compact(iban);
            return iban != null && (iban.StartsWith("CH") || iban.StartsWith("LI"));
        }

        /// <summary>
        /// True if the IBAN is a QR-IBAN: CH/LI IBAN whose institution identification (IID, positions 5-9)
        /// lies between 30000 and 31999.
        /// </summary>
        public static bool IsQrIban(string iban)
        {
            iban = Compact(iban);
            if (!IsChOrLiIban(iban) || iban.Length < 9)
                return false;

            return int.TryParse(iban.Substring(4, 5), out var iid) && iid >= 30000 && iid <= 31999;
        }

        /// <summary>Institution identification (IID) of a CH/LI IBAN without leading zeros, e.g. "700" for ZKB.</summary>
        public static string IidFromIban(string iban)
        {
            iban = Compact(iban);
            if (!IsChOrLiIban(iban) || iban.Length < 9)
                return null;

            var iid = iban.Substring(4, 5).TrimStart('0');
            return iid.Length == 0 ? null : iid;
        }

        /// <summary>QR reference: 27 digits, last digit is the recursive modulo 10 check digit.</summary>
        public static bool IsValidQrReference(string reference)
        {
            reference = Compact(reference);
            if (reference == null || reference.Length != 27 || !reference.All(char.IsDigit))
                return false;

            int[] table = { 0, 9, 4, 6, 8, 2, 7, 1, 3, 5 };
            var carry = 0;
            for (var i = 0; i < 26; i++)
                carry = table[(carry + (reference[i] - '0')) % 10];

            return (10 - carry) % 10 == reference[26] - '0';
        }

        /// <summary>ISO 11649 creditor reference: "RF" + 2 check digits + max. 21 alphanumeric characters.</summary>
        public static bool IsValidCreditorReference(string reference)
        {
            reference = Compact(reference);
            if (reference == null || reference.Length < 5 || reference.Length > 25)
                return false;
            if (!Regex.IsMatch(reference, "^RF[0-9]{2}[A-Z0-9]+$"))
                return false;

            return Mod97(reference.Substring(4) + reference.Substring(0, 4)) == 1;
        }

        /// <summary>Checks the restricted character set for reference elements (IG chapter 3.2).</summary>
        public static bool IsValidReference(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length > 35)
                return false;
            if (!ReferenceCharacters.IsMatch(value))
                return false;

            return !value.StartsWith(" ") && !value.StartsWith("/") && !value.EndsWith("/") && !value.Contains("//");
        }

        /// <summary>Checks the permitted SPS character set (IG chapter 3.1).</summary>
        public static bool IsValidSpsText(string value) => value == null || SpsCharacters.IsMatch(value);

        public static bool IsValidBic(string bic) =>
            bic != null && Regex.IsMatch(bic, "^[A-Z0-9]{4}[A-Z]{2}[A-Z0-9]{2}([A-Z0-9]{3})?$");

        public static bool IsValidLei(string lei) =>
            lei != null && Regex.IsMatch(lei, "^[A-Z0-9]{18}[0-9]{2}$");

        /// <summary>Country code of a BIC (positions 5-6).</summary>
        public static string BicCountry(string bic) => IsValidBic(bic) ? bic.Substring(4, 2) : null;

        private static int Mod97(string value)
        {
            var digits = string.Concat(value.Select(c => char.IsLetter(c) ? (c - 'A' + 10).ToString() : c.ToString()));
            return (int)(BigInteger.Parse(digits) % 97);
        }

        internal static void Require(bool condition, string message)
        {
            if (!condition)
                throw new ArgumentException(message);
        }
    }
}
