(function () {
    "use strict";

    function BeaconApiProxy() {
        this._events = [];

        this.pageview = function(visit_info, data) {


            var xmlHttp = new XMLHttpRequest();
            var domain = '/events/trigger-visit-event';

            if (visit_info.isNew) {
                if (data.product) {
                    visit_info.product_id = data.product.id;
                }

                xmlHttp.open("POST", domain);
                xmlHttp.setRequestHeader('Content-Type', 'application/json');
                xmlHttp.send(JSON.stringify(visit_info));
            }
        };
    }

    if (beacon_deferred) {
        beacon_deferred(new BeaconApiProxy());
    }
})();
