(ns clojure-lang.core
  (:gen-class))

(require 'clojure.test)

(defn -main
  "I don't do a whole lot ... yet."
  [& args]
  (println "Hello, World!"))

(defn solve [puzzle]
  puzzle)


(defn row [puzzle index]
  (let [row-start (- index (mod index 9))]
    (take 9 (drop row-start puzzle))
  )
)
  

(defn possibilities [row column square]
  (let [used (clojure.set/union row column square)]
	(clojure.set/difference (set (take 9 (drop 1 (range)))) used)
  )
)



; Unit tests

(deftest solve
  (is (= 
    (solve ".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57.")
	'())
  )
)

(deftest first-row
  (is (= 
    (row ".19.6..343.8.51...64...3.......194..47.8.5.19..564.......5...68...17.9.398..3.57." 0)
	'())
  )
)