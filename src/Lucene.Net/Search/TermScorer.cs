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

    using DocsEnum = Lucene.Net.Index.DocsEnum;
    using Similarity = Lucene.Net.Search.Similarities.Similarity;

    /// <summary>
    /// Expert: A <code>Scorer</code> for documents matching a <code>Term</code>.
    /// </summary>
#if FEATURE_SERIALIZABLE
    [Serializable]
#endif
    internal sealed class TermScorer : Scorer
    {
        private readonly DocsEnum docsEnum;
        private readonly Similarity.SimScorer docScorer;

        /// <summary>
        /// Construct a <code>TermScorer</code>.
        /// </summary>
        /// <param name="weight">
        ///          The weight of the <code>Term</code> in the query. </param>
        /// <param name="td">
        ///          An iterator over the documents matching the <code>Term</code>. </param>
        /// <param name="docScorer">
        ///          The </code>Similarity.SimScorer</code> implementation
        ///          to be used for score computations. </param>
        internal TermScorer(Weight weight, DocsEnum td, Similarity.SimScorer docScorer)
            : base(weight)
        {
            this.docScorer = docScorer;
            this.docsEnum = td;
        }

        public override int DocID
        {
            get { return docsEnum.DocID; }
        }

        public override int Freq
        {
            get { return docsEnum.Freq; }
        }

        /// <summary>
        /// Advances to the next document matching the query. <br>
        /// </summary>
        /// <returns> the document matching the query or NO_MORE_DOCS if there are no more documents. </returns>
        public override int NextDoc()
        {
            return docsEnum.NextDoc();
        }

        public override float GetScore()
        {
            Debug.Assert(DocID != NO_MORE_DOCS);
            return docScorer.Score(docsEnum.DocID, docsEnum.Freq);
        }

        /// <summary>
        /// Advances to the first match beyond the current whose document number is
        /// greater than or equal to a given target. <br>
        /// The implementation uses <seealso cref="docsEnum#advance(int)"/>.
        /// </summary>
        /// <param name="target">
        ///          The target document number. </param>
        /// <returns> the matching document or NO_MORE_DOCS if none exist. </returns>
        public override int Advance(int target)
        {
            return docsEnum.Advance(target);
        }

        public override long GetCost()
        {
            return docsEnum.GetCost();
        }

        /// <summary>
        /// Returns a string representation of this <code>TermScorer</code>. </summary>
        public override string ToString()
        {
            return "scorer(" + m_weight + ")";
        }
    }
}