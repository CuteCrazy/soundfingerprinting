﻿namespace SoundFingerprinting
{
    using System.Collections.Generic;

    using SoundFingerprinting.DAO;
    using SoundFingerprinting.DAO.Data;
    using SoundFingerprinting.Data;

    public abstract class AdvancedModelService : ModelService, IAdvancedModelService 
    {
        private readonly ISpectralImageDao spectralImageDao;

        private readonly ISubFingerprintDao subFingerprintDao;

        protected AdvancedModelService(
            ITrackDao trackDao,
            ISubFingerprintDao subFingerprintDao,
            ISpectralImageDao spectralImageDao)
            : base(trackDao, subFingerprintDao)
        {
            this.spectralImageDao = spectralImageDao;
            this.subFingerprintDao = subFingerprintDao;
        }

        public virtual void InsertSpectralImages(IEnumerable<float[]> spectralImages, IModelReference trackReference)
        {
            spectralImageDao.InsertSpectralImages(spectralImages, trackReference);
        }

        public virtual IEnumerable<SpectralImageData> GetSpectralImagesByTrackReference(IModelReference trackReference)
        {
            return spectralImageDao.GetSpectralImagesByTrackReference(trackReference);
        }

        public IList<HashedFingerprint> ReadHashedFingerprintsByTrack(IModelReference trackReference)
        {
            return subFingerprintDao.ReadHashedFingerprintsByTrackReference(trackReference);
        }
    }
}
