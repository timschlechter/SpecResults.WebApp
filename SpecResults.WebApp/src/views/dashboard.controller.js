(function() {
	"use strict";

	angular.module("app").controller("DashboardController", [
		"$scope",
		"$routeParams",
		"$location",
		"state",
		function($scope, $routeParams, $location, state) {
			$scope.report = state.getReport();

			$scope.isActiveFeature = function(feature) {
				return feature.id === $routeParams.featureId;
			};

			$scope.selectSearchResult = function($item, $model, $label) {
				delete $scope.query;

				var path = "/features/" + $item.feature.id + "/scenarios/" + $item.scenario.id;

				$location.path(path);
			};
		}
	]);
})();