class RegisterManager:
    def __init__(self, context):
        self.context = context
    def writeFloat (self, address, values):
        self.context[0].setValues(3, address, values)
