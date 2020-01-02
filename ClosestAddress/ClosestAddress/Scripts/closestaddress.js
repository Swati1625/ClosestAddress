if (document.getElementById("closest-address-results") != undefined) {
    var App = new Vue({
        el: '#closest-address-results',
        data: {
            message: 'Hello Vue!',
            result: ''
        },
        mounted: function () {
            this.dataURL = this.$el.getAttribute('data-dataURL');
            this.fetchData();
        },
        methods: {
            fetchData: function () {
                var self = this;
                axios.get(this.dataURL)
                    .then(function (response) {
                        self.result = response.data;
                    });
            }
        }
    });
}