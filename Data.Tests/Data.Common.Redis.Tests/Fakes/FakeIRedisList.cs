namespace ProcessingTools.Data.Common.Redis.Tests.Fakes
{
    using System;
    using System.Collections.Generic;
    using ServiceStack.Redis;

    public class FakeIRedisList : List<string>, IRedisList
    {
        public string Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Append(string value)
        {
            throw new NotImplementedException();
        }

        public string BlockingDequeue(TimeSpan? timeOut)
        {
            throw new NotImplementedException();
        }

        public string BlockingPop(TimeSpan? timeOut)
        {
            throw new NotImplementedException();
        }

        public string BlockingRemoveStart(TimeSpan? timeOut)
        {
            throw new NotImplementedException();
        }

        public string Dequeue()
        {
            throw new NotImplementedException();
        }

        public void Enqueue(string value)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAll()
        {
            return this;
        }

        public List<string> GetRangeFromSortedList(int startingFrom, int endingAt)
        {
            throw new NotImplementedException();
        }

        public string Pop()
        {
            throw new NotImplementedException();
        }

        public string PopAndPush(IRedisList toList)
        {
            throw new NotImplementedException();
        }

        public void Prepend(string value)
        {
            throw new NotImplementedException();
        }

        public void Push(string value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            this.RemoveRange(0, this.Count);
        }

        public string RemoveEnd()
        {
            throw new NotImplementedException();
        }

        public string RemoveStart()
        {
            throw new NotImplementedException();
        }

        public long RemoveValue(string value)
        {
            throw new NotImplementedException();
        }

        public long RemoveValue(string value, int noOfMatches)
        {
            throw new NotImplementedException();
        }

        public void Trim(int keepStartingFrom, int keepEndingAt)
        {
            throw new NotImplementedException();
        }
    }
}
