using System;
using SimpleListProject;

namespace Figures {
	class SimpleStack<T> : SimpleList<T> where T : IComparable {
		public void Push(T el) {
			Add(el);
		}
		public T Pop() {
			T Result = default(T);
			if (this.Count == 0) return Result;
            
			if (this.Count == 1) {
				Result = this.first.data;
				this.first = null;
		                this.last = null;
            		}
			else {
				SimpleListItem<T> newLast = this.GetItem(this.Count - 2);
		                Result = newLast.next.data;
               			this.last = newLast;
		                newLast.next = null;
	    		}
	    		this.Count--;
			return Result;
		}
	}
}
