import heapq
from collections import defaultdict
import math

class Huffman:

    def encode(text):
        """
        Text coding using Huffman codes.
        """
        frequency = calculate_frequency(text)
        # Build heap based on character frequency
        heap = build_heap(frequency)
        # Build a Huffman tree
        tree = build_tree(heap)
        # Get Huffman character codes
        huff_codes = huffman_code(tree)

        encoded_text = ""
        for char in text:
            encoded_text += huff_codes[char]
        return encoded_text

    def decode(code_table, s):
        decoded = ''
        while len(s) > 0:
            i = 0
            acc = ''
            while i < len(s):
                current_char = s[i]
                acc += current_char
                if acc in code_table.values():
                    new_dict = dict(zip(code_table.values(), code_table.keys()))
                    acc = new_dict[acc]
                    i += 1
                    break
                i += 1
            s = s[i:]
            decoded += acc
        return decoded

    def calculate_frequency(my_text):
        frequency = defaultdict(int)
        for character in my_text:
            frequency[character] += 1
        return frequency

    def build_heap(freq):
        heap = [[weight, [char, ""]] for char, weight in freq.items()]
        heapq.heapify(heap)
        return heap

    def build_tree(heap):
        while len(heap) > 1:
            lo = heapq.heappop(heap)
            hi = heapq.heappop(heap)
            for pair in lo[1:]:
                pair[1] = '1' + pair[1]
            for pair in hi[1:]:
                pair[1] = '0' + pair[1]
            heapq.heappush(heap, [lo[0] + hi[0]] + lo[1:] + hi[1:])
        return heap[0]

    def huffman_code(tree):
        huff_code = {}
        for pair in tree[1:]:
            char = pair[0]
            code = pair[1]
            huff_code[char] = code
        return huff_code

