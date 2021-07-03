import XCTest

class SolverTests: XCTestCase {
    let data = ".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57."
    let puzzle = data.characters.map({
        (square: Character) -> Int? in Int(String(square))
    })

    func testGetRow() {
        assert(false, "Failed")    
    }
}