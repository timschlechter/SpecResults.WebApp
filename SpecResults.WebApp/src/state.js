(function() {
	"use strict";

	angular.module("app").factory("state", [
		"Report",
		function(Report) {
			var _report = new Report(reportData);

			return {
				getReport: function() {
					return _report;
				}
			};
		}
	]);
})();