using System;

namespace NLPLib.Search.Scores
{
    public class Bm25 : IScore
    {
        private readonly double k1 = 1.2;
        private readonly double b = 0.75;
        private readonly int _numberOfDocuments;
        private readonly double _AvgNumberOfTermsPrDocument;

        public Bm25(int numberOfDocuments, double AvgNumberOfTermsPrDocument)
        {
            _numberOfDocuments = numberOfDocuments;
            _AvgNumberOfTermsPrDocument = AvgNumberOfTermsPrDocument;
        }

        /// <summary>
        /// idf, computed as log(1 + (N - n + 0.5) / (n + 0.5))
        /// </summary>
        /// <param name="numberOfDocumentsWithTerm"></param>
        /// <returns></returns>
        private double Idf(int numberOfDocumentsWithTerm)
        {
            var t = _numberOfDocuments - numberOfDocumentsWithTerm + 0.5;
            var b = numberOfDocumentsWithTerm + 0.5;
            return Math.Log10(1 + (t / b));
        }

        /// <summary>
        /// tf, computed as freq / (freq + k1* (1 - b + b* dl / avgdl)) from:
        ///5.0 = freq, occurrences of term within document
        ///1.2 = k1, term saturation parameter
        ///0.75 = b, length normalization parameter
        ///17.0 = dl, length of field
        ///22.666666 = avgdl, average length of field
        /// </summary>
        /// <returns></returns>
        private double Td(int documnetId, int termHitsInDocument, int numberOfTermsForDoc)
        {
            var tf = termHitsInDocument;
            var dl = numberOfTermsForDoc;
            return tf / (tf + k1 * (1 - b + b * dl / _AvgNumberOfTermsPrDocument));
        }

        /// <summary>
        /// idf, computed as log(1 + (N - n + 0.5) / (n + 0.5))
        /// </summary>
        /// <param name="numberOfDocumentsWithTerm"></param>
        /// <param name="termHitsInDocument"></param>
        /// <param name="numberOfTermsInDocumnt"></param>
        /// <returns></returns>
        public double Score(int documentId, int numberOfDocumentsWithTerm, int termHitsInDocument, int numberOfTermsForDoc)
        {
            var idf = Idf(numberOfDocumentsWithTerm);
            var td = Td(documentId, termHitsInDocument, numberOfTermsForDoc);
            return idf * td;
        }
    }
}