using LessonMonitor.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace LessonMonitor.BusinessLogic
{
    public class HomeworksService : IHomeworksService
    {
        public const string HOMEWORK_IS_INVALID = "homework is invalid";
        private readonly IHomeworksRepository _homeworksRepository;
        public HomeworksService(IHomeworksRepository homeworksRepository)
        {
            _homeworksRepository = homeworksRepository;
        }

        public bool Create(Homework homework)
        {
            if (homework is null)
            {
                throw new ArgumentNullException(nameof(homework));
            }
            var isInvalid = string.IsNullOrWhiteSpace(homework.Link)
                || string.IsNullOrWhiteSpace(homework.Title)
                || homework.MemberId <= 0;


            if (isInvalid)
            {
                throw new BusinessException(HOMEWORK_IS_INVALID);
            }

            _homeworksRepository.Add(homework);
            return true;
        }

        public bool Delete(int homeworkId)
        {
            _homeworksRepository.Delete(homeworkId);
            return true;
        }

    }
}
