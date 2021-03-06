using System;
using System.Diagnostics;

namespace Lucene.Net.Search
{
    /*
     * Licensed to the Apache Software Foundation (ASF) under one or more
     * contributor license agreements.  See the NOTICE file distributed with
     * this work for additional information regarding copyright ownership.
     * The ASF licenses this file to You under the Apache License, Version 2.0
     * (the "License"); you may not use this file except in compliance with
     * the License.  You may obtain a copy of the License at
     *
     *     http://www.apache.org/licenses/LICENSE-2.0
     *
     * Unless required by applicable law or agreed to in writing, software
     * distributed under the License is distributed on an "AS IS" BASIS,
     * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     * See the License for the specific language governing permissions and
     * limitations under the License.
     */

    using BytesRef = Lucene.Net.Util.BytesRef;

    /// <summary>
    /// Contains statistics for a specific term
    /// @lucene.experimental
    /// </summary>
#if FEATURE_SERIALIZABLE
    [Serializable]
#endif
    public class TermStatistics
    {
        private readonly BytesRef term;
        private readonly long docFreq;
        private readonly long totalTermFreq;

        public TermStatistics(BytesRef term, long docFreq, long totalTermFreq)
        {
            Debug.Assert(docFreq >= 0);
            Debug.Assert(totalTermFreq == -1 || totalTermFreq >= docFreq); // #positions must be >= #postings
            this.term = term;
            this.docFreq = docFreq;
            this.totalTermFreq = totalTermFreq;
        }

        /// <summary>
        /// returns the term text </summary>
        public BytesRef Term
        {
            get { return term; }
        }

        /// <summary>
        /// returns the number of documents this term occurs in </summary>
        /// <seealso cref= TermsEnum#docFreq()  </seealso>
        public long DocFreq
        {
            get { return docFreq; }
        }

        /// <summary>
        /// returns the total number of occurrences of this term </summary>
        /// <seealso cref= TermsEnum#totalTermFreq()  </seealso>
        public long TotalTermFreq
        {
            get { return totalTermFreq; }
        }
    }
}