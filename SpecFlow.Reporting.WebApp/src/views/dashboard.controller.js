(function () {
	'use strict';

	angular.module('app').controller('DashboardController', [
        '$scope',
        '$routeParams',
        'state',
        function ($scope, $routeParams, state) {
            $scope.report = state.getReport();

        	$scope.isActiveFeature = function (feature) {
        		return feature.id === $routeParams.featureId;
        	};
        }
	]);
})();