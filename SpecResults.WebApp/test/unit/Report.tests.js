describe("report", function() {
	var Report;

	beforeEach(module("app"));

	beforeEach(inject(function(_Report_) {
		Report = _Report_;
	}));

	it("should not contain duplicate ids", function() {
		var report = new Report(reportData);

		var allItems = _.union(
			report.features,
			report.scenarios,
			report.scenarioBlocks,
			report.steps
		);

		var groups = _.groupBy(allItems, "id");

		_.forEach(groups, function(val, key) {
			expect(val.length).toBe(1);
		});
	});
});