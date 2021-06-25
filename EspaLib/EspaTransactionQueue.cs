using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EspaLib
{
    public class EspaTransactionsQueue
    {
        #region member data       
        private Queue<object> transactions;
        #endregion

        #region properties       
        public Queue<object> Transactions
        {
            get { return transactions; }
        }
        #endregion

        public EspaTransactionsQueue()
        {
            transactions = new Queue<object>();
        }

        /// <summary>
        /// returns if the transactions queue is currently empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return (transactions.Count <= 0);
        }

        /// <summary>
        /// deletes all transaction currently in the transactions queue
        /// </summary>
        public void Clear()
        {
            try
            {
                transactions.Clear();
            }
            catch (Exception exc)
            {
                Console.WriteLine("EspaTransactionsQueue:Clear: ERROR:{0}", exc.Message);
            }
        }

        /// <summary>
        /// adds an transaction at the end of the transactions queue
        /// </summary>
        /// <param name="newTransation"></param>
        /// <returns></returns>
        public bool Add(object newTransation)
        {
            try
            {
                transactions.Enqueue(newTransation);
            }
            catch (Exception exc)
            {
                Console.WriteLine("TransactionQueue:Add: ERROR:{0}", exc.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// removes and returns the transaction at the beginning of the queue
        /// </summary>
        /// <returns></returns>
        public object GetNext(bool RemoveIt=true)
        {
            try
            {
                if (IsEmpty()) return null;

                if (RemoveIt) return transactions.Dequeue();
                else return transactions.Peek();
            }
            catch (Exception exc)
            {
                Console.WriteLine("EspaTransactionsQueue:GetNextTransaction: ERROR:{0}", exc.Message);
                return null;
            }
        }

        /// <summary>
        /// removes the transaction at the beginning of the queue
        /// </summary>
        /// <returns></returns>
        public void DeleteNext()
        {
            try
            {
                if (IsEmpty()) return;
                transactions.Dequeue();
            }
            catch (Exception exc)
            {
                Console.WriteLine("EspaTransactionsQueue:DeleteNext: ERROR:{0}", exc.Message);
                return;
            }
        }


    }
}
