namespace SoundFingerprinting.LCS
{
    using System.Collections.Generic;
    using System.Linq;

    using SoundFingerprinting.Configuration;
    using SoundFingerprinting.DAO.Data;
    using SoundFingerprinting.Query;

    internal class QueryResultCoverageCalculator : IQueryResultCoverageCalculator
    {
        public IEnumerable<Coverage> GetCoverages(TrackData trackData, GroupedQueryResults groupedQueryResults, QueryConfiguration configuration)
        {
            var fingerprintConfiguration = configuration.FingerprintConfiguration;
            var matches = groupedQueryResults.GetMatchesForTrack(trackData.TrackReference).ToList();

            System.Diagnostics.Trace.WriteLine("\n ---------- ");
            System.Diagnostics.Trace.WriteLine("Match list: ");
            foreach (var match in matches)
            {
                System.Diagnostics.Trace.WriteLine("MatchAt: " + match.QueryMatchAt + " / " + match.TrackMatchAt + " SequenceNum: " + match.QuerySequenceNumber + " / " + match.TrackSequenceNumber + " --- score: " + match.Score);
            }
            System.Diagnostics.Trace.WriteLine(" ---------- \n");

            if (!matches.Any())
            {
                return Enumerable.Empty<Coverage>();
            }
            
            double queryLength = groupedQueryResults.QueryLength;

            if (configuration.AllowMultipleMatchesOfTheSameTrackInQuery)
            {
                return matches.EstimateIncreasingCoverages(queryLength, trackData.Length,
                    fingerprintConfiguration.FingerprintLengthInSeconds, configuration.PermittedGap);
            }

            return new[] { matches.EstimateCoverage(queryLength, trackData.Length, fingerprintConfiguration.FingerprintLengthInSeconds, configuration.PermittedGap) };
        }
    }
}
