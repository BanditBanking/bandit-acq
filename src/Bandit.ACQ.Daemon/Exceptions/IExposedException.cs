using Bandit.ACQ.Daemon.Models.DTOs;

namespace Bandit.ACS.Daemon.Exceptions
{
    public interface IExposedException
    {
        ProblemDetailDTO Expose();
    }
}
