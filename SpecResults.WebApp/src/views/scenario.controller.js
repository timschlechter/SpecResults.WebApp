(function() {
	"use strict";

	angular.module("app").controller("ScenarioController", [
		"$scope",
		"$routeParams",
		"state",
		function($scope, $routeParams, state) {
			var report = state.getReport();
			$scope.feature = report.findFeatureById($routeParams.featureId);
			$scope.scenario = report.findScenarioById($routeParams.scenarioId);
		}
	]);
})();