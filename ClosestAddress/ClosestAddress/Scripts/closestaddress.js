if (document.getElementById("closest-address-results") !== undefined) {
    var App = new Vue({
        el: '#closest-address-results',
        data: {
            results: [],
            searchAddress:''
        },
        mounted: function () {
            this.dataURL = this.$el.getAttribute('data-dataURL');
           
        },
        methods: {
            fetchData: function () {
                this.dataURL += "?originAddress=" + this.searchAddress;
                var self = this;
                axios.get(this.dataURL)
                    .then(function (response) {
                        self.results = response.data.AddressResults;
                    });
            },
            searchClick: function () {
                if (this.searchAddress !== "") {
                    this.fetchData();
                }
            }
        }
    });
}