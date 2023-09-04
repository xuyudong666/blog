using SwiftCode.BBS.IServices.Base;
using SwiftCode.BBS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BBS.IServices;

public interface IQuestionServices : IBaseServices<Question>
{
    Task<Question> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<Question> GetQuestionDetailsAsync(int id, CancellationToken cancellationToken = default);

    Task AddQuestionComments(int id, int userId, string content, CancellationToken cancellationToken = default);

}
