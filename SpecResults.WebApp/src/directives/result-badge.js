(function(angular) {
	"use strict";

	angular.module("app").directive("resultBadge", [
		"$modal", function($modal) {
			return {
				restrict: "EA",
				replace: true,
				templateUrl: "directives/result-badge.tpl.html",
				scope: {
					'reportItem': "="
				},
				link: function($scope, element, attrs) {
					$scope.showException = function(event) {

						if (event) {
							event.stopPropagation();
							event.preventDefault();
						}

						var modalInstance = $modal.open({
							templateUrl: "templates/exception-details-modal.tpl.html",
							size: "lg",
							controller: function($scope, $modalInstance, exception) {
								$scope.exception = exception;

								$scope.ok = function() {
									$modalInstance.close();
								};
							},
							resolve: {
								exception: function() {
									return $scope.reportItem.exception;
								}
							}
						});
					};
				}
			};
		}
	]);
})(this.angular);