import _ from "lodash"
const theMixin = {
    data() {
        return {
            centerFormVertically: false,
            elemToCenter: null
        }
    },
    mounted: function () {
        if (this.centerFormVertically) {
            this.$nextTick(function () {
                var parentElems=this.getAllParentElementsOf(this.elemToCenter);
                _.forEach(parentElems, $$el => {
                        $$el.classList.add("h-100"); //set height of all parent divs to 100%, or "h-100" which is a bootstrap equivalent
                    });
                
                // .forEach(
                //     x => x.
                // )
                // alert(this.getAllParentElementsOf(elem)).toString
            })
        }
    },
    methods: {
        getAllParentElementsOf(elem) {
            var parents = [];
            var $$child = null;
            var typeOfElem = typeof elem;
            //var error = "";

            switch (typeOfElem) {
                case 'string':
                    $$child = document.querySelector(elem);
                    break;
                case 'undefined':
                    break;
                case 'object':
                    $$child = elem;
                    break;
            }

            if ($$child !== null && typeof $$child === 'object')  {
                // Push each parent element to the array
                for ( ; $$child && $$child !== document; $$child = $$child.parentNode ) {
                    parents.push($$child);
                }
            }

            // Return our parent array
            return parents;
            //};

        }

    }
}

export default theMixin;